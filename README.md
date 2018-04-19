<center><img src="https://i.imgur.com/iLqILvT.png" height=100 width=100></img></center>

# Measuring-Speech
MTSU Software Engineering CSCI-4700/5700 project with <a href="http://research.vuse.vanderbilt.edu/rasl/pase-cane-homepage/">Vanderbilt</a>

<img src="https://cdn0.iconfinder.com/data/icons/tuts/256/slack_alt.png" height="20" width="20"><a href="https://mtsu-csci4700-speech.slack.com"> Slack</a></img>

## Main goals
1. Measure how many words are in an audio stream/file.
2. Who is the speaker? Adult or child.
3. Content of the sentence. Is it a sentence, is it a question?

## Other goals
1. Quantify when and whether a child likes or dislikes aspects.

# Installing dependences  
### Install the python package manager PIP  

```python<3> get-pip.py``` or ```sudo easy_install pip```  

__Most system use Python 2.7 as the default python interpreter.__ Be sure to check which python you're using with ```python --version``` if you are unsure of your system's default python interpreter.  You more than likely need to use ```python3```.  Pip might be available via brew for macOS: ```brew install pip3``` will likely work.  I also think there maybe a way to get pip via packages such as ```sudo apt install python3-pip``` in most Ubuntu/apt based systems.  You may also use ```pip --version``` to display which python pip will use.

### Install virutalenv

install virtualenv using pip:  

```pip<3> install virtualenv```

virtualenv is used to create and manage environments for different python projects.  Use virtualenv to create a virtual environment by using:

```virtualenv env```

__NOTE: If you are in a virtual environment -- do not, EVER, use ```sudo```, as this will not install into the virutal environment. In a virtual environment, the packages are installed in (this case) to the folder ```env```__

### Switching environments
Use ```source env/bin/activate``` to load your environment.  ```env``` is a placeholder for your environment name.  

use ```deactivate``` to exit out of your environment.  

To verify which environment you're in, use ```which pip```.  if you see that the pip location is in your environment folder (env), then you are in your virtual environment.  Also notice that in virtualenv, python3 is now the default interpreter which makes life much easier.  Check using ```python --version``` and notice the __lack__ of 3 at the end.

### Install packages and requirements:

Then its as easy as:  
```pip install -r requirements.txt```  

Be sure to download the NLTK dataset: use ```python<3>``` to bring up the interpreter:  
```import nltk```
```nltk.download()```  

```swig``` is a dependance for some python packages.  Use ```apt```, ```brew```, or ```choco``` to ```install swig``` 

