# IPAustralia
This is service for advanced searching in https://search.ipaustralia.gov.au by trademark name
instead of browsing the site, filling the textbox, making search and going through pages, you can pass the trademark name as a parameter to the current applciation and will get all related data at ones.

To use the application, first start it.
If it is started as IIS default domain will be at localhost:44319 (https) or  localhost:26813 (http)
else if is started as Kestrel servis domain will be at localhost:7071 (https) or  localhost:5255 (http)

for now service has only one endpoint at 'api/v1/search'
The endpoint recieves requests only by GET method
The endpoint accepts a variable by name TrademarkName as query string parameter
 
search examples
{schema}://{domain}:{port}/api/v1/search?trademarkname=abc
{schema}://{domain}:{port}/api/v1/search?trademarkname=good