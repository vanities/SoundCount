FROM ubuntu:16.04

RUN apt-get update -y

RUN	apt-get install -y python3 python3-pip python3-dev swig pulseaudio libpulse-dev locales locales-all

COPY ./requirements.txt /app/requirements.txt

WORKDIR /app

ENV LANG en_US.UTF-8  
ENV LANGUAGE en_US:en  
ENV LC_ALL en_US.UTF-8     

RUN pip3 install --upgrade pip setuptools

RUN pip3 install -r requirements.txt

COPY . /app

ENTRYPOINT [ "python3" ]

CMD [ "voice-analyzer.py" ]