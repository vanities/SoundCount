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

## Dependencies for python3
1. <a href="http://www.portaudio.com/">PortAudio</a> >= 19
1. <a href="https://people.csail.mit.edu/hubert/pyaudio/">PyAudio</a> >= 0.2.11
2. <a href="https://pypi.python.org/pypi/SpeechRecognition/">SpeechRecognition</a> >= 3.8.1
3. <a href="http://www.swig.org/download.html">swig</a> >= 3.0.12
3. <a href="https://github.com/cmusphinx/pocketsphinx">PocketSphinx</a> >= 0.1.3 (for offline recognition)
