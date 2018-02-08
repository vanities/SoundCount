#!/usr/bin/env python

import speech_recognition as sr


""" Audio Counter Class:
    Initializes to capture and classify the number of words captured



"""
class Counter:

    ## initialization:
    def __init__(self):

        # capture the aurdio using the microphone
        # assign to audio
        self.capture()

        # recognize the sentence and 
        # assign to sentence
        self.recognize()

        # count the words in the sentence
        # assign to word_count
        self.count()



    def capture(self):
        # obtain audio from the microphone
        self.r = sr.Recognizer()
        with sr.Microphone() as source:
           print("Say something!")
           self.audio = self.r.listen(source)


    def recognize(self):
        try:
           # for testing purposes, we're just using the default API key
           # to use another API key, use `r.recognize_google(audio, key="GOOGLE_SPEECH_RECOGNITION_API_KEY")`
           # instead of `r.recognize_google(audio)`
           self.sentence = self.r.recognize_sphinx(self.audio)
           print("Google Speech Recognition thinks you said: " + self.sentence)
        except sr.UnknownValueError:
           print("Google Speech Recognition could not understand audio")
        except sr.RequestError as e:
           print("Could not request results from Google Speech Recognition service; {0}".format(e))


    def count(self):
        # split the sentence in between spaces
        self.word_count = len(self.sentence.split())
        print("Words counted: ", self.word_count)


# formal instantiaztion of the speech counter class
def main():
    c = Counter()


# calls main if this file is named "main"
if __name__ == '__main__':
    main()