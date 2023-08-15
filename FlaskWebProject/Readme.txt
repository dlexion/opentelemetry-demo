to activate virtual environment (optional, commands for Windows):
python -m venv env
.\env\Scripts\activate
to deactivate:
deactivate

install dependencies:
pip install -r requirements.txt

run application:
python app.py

to save requirements (in case of update):
pip freeze > requirements.txt

does not work:
docker build -t python-otl-sample .
docker run -d -p 5555:8080 python-otl-sample

kubectl apply -f k8s.yaml