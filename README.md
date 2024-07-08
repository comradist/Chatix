# Chatix
Is simple chat app based on http API and websocket. Clean architecture, Repository pattern, MySql, NLog, mediatR, CQRS, SignalR, .net 8, RESTful API

## Scheme of dependencies in the application
![Screenshot from 2024-07-08 22-39-49](https://github.com/comradist/Chatix/assets/81923026/824e8243-4bcb-4b3c-aeb0-1c2b89a7b775)

- Contract, Shared, Entity is Core layer - all fundamentally important entities, as well as configurations, DTOs, contracts
- Domain(or application) layer - all main business logic based on use cases  
- API, API representation is Presentation layer - can be attributed to the infrastructure, here the API controllers and the configuration of external resources
- Infrastructure, Persistence is Infrastructure layer - external resources for different purposes, like login, mail, repository pattern and so.  
## Installation

I will add soon

```bash
docker 
```

## Usage

You can create a chat and send messages in real time. To do this, you need to use the SignalR lib on the client or form a message according to its protocol via webSocket. 

Example of join to chat (Postman implementation):
```JSON
{"protocol":"json","version":1}

{
    "type":1,
    "target":"Join",
    "arguments":[{"roomId": "08dc9f20-fc45-4644-8df3-eced1ff7ab38", "userId": "08dc9eb8-d7c1-410c-8a2b-d9ed60b55065"}
    ]
}
```
Target explanation:
- "CloseRoom" - close chat for every one users which in the chat
- "LeaveRoom" - leave chat by concrete user
- "CloseRoomsByAdmin" - cascade delete all chats by admin user, if user is deleted
- and so on, check the ChatHub.cs

Also you can check swagger with documented RESTful API for every entity
- http://localhost:5000/swagger/index.html

## License

[MIT](https://choosealicense.com/licenses/mit/)
