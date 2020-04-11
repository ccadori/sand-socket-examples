const Sand = require('sand-socket');

const server = new Sand();
server.verboseLog = true;

server.on('connected', (client) => {
  
  // When client send its nickname
  client.on('nickname', nickname => {
    client.nickname = nickname;
    server.writeToAll('message', JSON.stringify({
      nickname: "System",
      message: `${client.nickname} has entered the chat.`
    }), client.id);
  });
  
  // When client send a message
  client.on('message', message => {
    server.writeToAll('message', JSON.stringify({
      nickname: client.nickname,
      message
    }));
  });

  // When client disconnects
  client.on('disconnected', () => {
    server.writeToAll('message', JSON.stringify({
      nickname: "System",
      message: `${client.nickname} has disconnected to the chat.`
    }), client.id)
  })
});

server.listen();
