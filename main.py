#!/usr/bin/env python

import speech_recognition as sr


""" Audio Counter Class:
    Initializes to capture and classify the number of words captured



"""
class Counter:

    ## initialization:
    def __init__(self):

        self.capture()
        self.recognize()
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

           self.sentence = self.r.recognize_google(self.audio)
           print("Google Speech Recognition thinks you said: " + self.sentence)
        except sr.UnknownValueError:
           print("Google Speech Recognition could not understand audio")
        except sr.RequestError as e:
           print("Could not request results from Google Speech Recognition service; {0}".format(e))


        """
        if words == ' ' or  self.letter_count > len(
    def count(self):

        self.word_count = 0
        #self.letter_count = 0

        self.word_count = len(self.sentence.split())Za  

self.sentence):
            self.word_count += 1

        self.letter_count += 1
        """ 
        #print("Actual letters in sentence: ", len(self.sentence))
        #print("Counted letters in sentence: ", self.letter_count)
        print("Words counted: ", self.word_count)


# formal instantiaztion of the speech counter class
def main():
    c = Counter()


# calls main if this file is named "main"
if __name__ == '__main__':
    main()