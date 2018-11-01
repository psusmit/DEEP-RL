from chatterbot.trainers import ListTrainer
from chatterbot import ChatBot
bot=ChatBot('test')
# any readable chats
conv=open('whatsappChinya.csv','r').readlines()
bot.set_trainer(ListTrainer)
bot.train(conv)

while True:
    req=input('you: ')
    res=bot.get_response(req)
    print('bot :' ,res)