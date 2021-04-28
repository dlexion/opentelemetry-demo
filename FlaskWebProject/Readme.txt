install dependencies (preferably cmd/powershell in admin mode):

pip install opentelemetry-api
pip install opentelemetry-sdk
pip install opentelemetry-proto
pip install opentelemetry-instrumentation
pip install opentelemetry-exporter-otlp-proto-grpc
pip install opentelemetry-instrumentation-flask
pip install opentelemetry-instrumentation-requests
pip install opentelemetry-instrumentation-logging

pip install git+https://github.com/open-telemetry/opentelemetry-python.git@metrics#subdirectory=exporter/opentelemetry-exporter-opencensus

pip install opentelemetry-api==1.10a0
pip install opentelemetry-sdk==1.10a0

run application:
python app.py