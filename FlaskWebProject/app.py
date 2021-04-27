"""
This script runs the application using a development server.
It contains the definition of routes and views for the application.
"""

from flask import Flask
app = Flask(__name__)

# Make the WSGI interface available at the top level so wfastcgi can get it.
wsgi_app = app.wsgi_app

import requests

from opentelemetry import trace
from opentelemetry.exporter.opencensus.trace_exporter import OpenCensusSpanExporter
from opentelemetry.exporter.otlp.proto.grpc.trace_exporter import OTLPSpanExporter
from opentelemetry.sdk.trace import TracerProvider
from opentelemetry.sdk.trace.export import (
    BatchSpanProcessor,
    SimpleSpanProcessor,
    )
from opentelemetry.sdk.resources import SERVICE_NAME, Resource
from opentelemetry.instrumentation.flask import FlaskInstrumentor
from opentelemetry.instrumentation.requests import RequestsInstrumentor

# from opentelemetry import metrics
# from opentelemetry.exporter.opencensus.metrics_exporter import (
#     OpenCensusMetricsExporter,
# )
# from opentelemetry.sdk.metrics import MeterProvider

import logging
from opentelemetry.instrumentation.logging import LoggingInstrumentor

tracer_provider = TracerProvider(resource=Resource.create({SERVICE_NAME: "python_service_traces"}))
trace.set_tracer_provider(tracer_provider)
exporter = OTLPSpanExporter(endpoint="http://localhost:4317")
span_processor = BatchSpanProcessor(exporter)
tracer_provider.add_span_processor(span_processor)

tracer = trace.get_tracer(__name__)
logger = logging.getLogger(__name__)

FlaskInstrumentor().instrument_app(app)
RequestsInstrumentor().instrument()
LoggingInstrumentor().instrument(set_logging_format=True, logging_format=
"""timestamp:\t%(asctime)s
log_level:\t%(levelname)s
name:\t%(name)s
filename:\t%(filename)s:%(lineno)d
trace_id:\t%(otelTraceID)s
span_id:\t%(otelSpanID)s
service.name:\t%(otelServiceName)s
message:\t%(message)s\n""")

@app.route('/')
def hello():
    flask_span = trace.get_current_span()
    flask_span.set_attribute("custom_info", "add more attributes to the flask instrumented span")
    logger.info("info inside flask instrumented span")
    with tracer.start_as_current_span("example-request") as span:
        span.set_attribute("is_example", "yes :)")
        span.add_event('Python event')
        logger.warning("warning inside custom span")
        requests.get("http://www.example.com")
    return "Hello from Python app!"

if __name__ == '__main__':
    import os
    HOST = os.environ.get('SERVER_HOST', 'localhost')
    try:
        PORT = int(os.environ.get('SERVER_PORT', '5555'))
    except ValueError:
        PORT = 5555
    app.run(HOST, PORT)
