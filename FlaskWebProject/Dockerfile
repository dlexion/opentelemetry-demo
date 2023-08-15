# set base image (host OS)
FROM python:3.9

# set the working directory in the container
WORKDIR /code

# copy the dependencies file to the working directory
COPY requirements.txt .

# install dependencies
RUN pip install --no-deps -r requirements.txt

# copy the content of the local src directory to the working directory
COPY app.py .

# command to run on container start
CMD [ "python", "./app.py" ]