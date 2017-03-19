Hi there,

Thanks for taking some time to have a look at my solution. I really appreciate your feedback whether you like it or not.

Here is a list of important points which can make the solution easier to read and understand, and some assumptions about the problem as well:
- I have used the CQRS pattern
- I could have created commands, queries and queryResults for each command and query, and overload the execute for commands and handle for queries. However, for simplicity, I would rathered to keep on a simple way of having a separate method with a separate name for each command and query.
- In CQRS, when creating an object, it is very common to generate the key in the UI and send it to back end. However, here since the db generates the key for Movie in the ReadRepo I maintain a dictionary to map the the UI generated GUID to actual Movie objects. The purpose is, if user wants to update the Movie as soon as they create one, they should be able to send the Temp GUID as a part of the Object.
- I assumed the number of reads would be much greater than the number of writes. So, I have introduced a caching mechanism in the ReadRepositoy, thus I would rather the ReadRepositoy to be a singleton class and only one instance of it throughout the lifecycle of the application.
- I have used Simple Injector for DI and relied on Singleton mechanism of it for the ReadRepository (Application_Start).
- For testing, I have used MS Test (even though I preferred xUnit) and Moq.
- The service calls could be async and there could be a mechanism of event sourcing or queueing at least for commands. But, I didn't have enough time to work on that. So, I kept it simple.
- In the ReadRepository, I have used  ConcurrentBag and ConcurrentDictionary for caches for the sake of thread safety.
- I have assumed search functionality is meant to be across all of the fields Movie and at the same time. So, when someone searches for 'Sci-fi', the result could contain movies which have the word in their genre or in their titles (etc.).
- Because of the lack of time, I haven't completed some of the functionalities (Update mechanism which results in some amendment in cache lists), and some of the tests as well. But, I have left some comments as TODO.
- If you need to run the service or the tests, add the MovieLibrary.dll manually in the references of the project.

Sorry if there is any mistake here in the ReadMe.txt as it is very late at night and I am too sleepy to write a text or code :)

Many thanks
Hemen