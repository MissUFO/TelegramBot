
How to deploy it:

1. Create a bot in telegram
2. Change TELEGRAM_API_KEY in web config
3. Publish web service in internet
4. Register web hook by running post command with api key and web service url:
https://api.telegram.org/bot327682208:AAHv2ALZWBo48HjNH347rJZ3-r_bTwznaBQ/setWebhook?url=https://adidasretailtelegrambot.azurewebsites.net/api/webhook&max_connections=100
5. Try it.

External libs:
Install-Package Spire.BarCode