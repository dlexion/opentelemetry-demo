install dependencies (preferably cmd/powershell in admin mode):

pip install requests
pip install flask
pip install psutil

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

pip install --force-reinstall --no-deps opentelemetry-exporter-opencensus
pip install --force-reinstall --no-deps opentelemetry-exporter-otlp-proto-grpc==1.10a0

TBD: install corrct version of exporters rigt away

run application:
python app.py