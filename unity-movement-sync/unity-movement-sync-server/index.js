const Sand = require('sand-socket');

const server = new Sand();
server.verboseLog = true;

server.on('connected', client => {

  // Setting client nickname
  client.on('nickname', nickname => {
    client.nickname = nickname;
    client.position = { y: 0, x: 0 };
    client.inGame = true;
    client.updatePosition = true;
  });

  // On game scene is loaded
  client.on('in-game', () => {
    // Sending to other clients about the new client
    server.writeToAll("new-client", JSON.stringify({
      id: client.id,
      nickname: client.nickname,
      x: client.position.x,
      y: client.position.y,
    }), client.id);

    // Sending to the new client the other ones
    for (let c of server.clients) {
      if (!c.inGame || c.id === client.id) continue;
      
      client.write("new-client", JSON.stringify({
        id: c.id,
        nickname: c.nickname,
        x: c.position.x,
        y: c.position.y,
      }));
    }
  });

  // Receiving position from client
  client.on('position', position => {
    client.updatePosition = true;
    client.position = JSON.parse(position);

    server.writeToAll("position", JSON.stringify({ 
      id: client.id, 
      x: client.position.x, 
      y: client.position.y 
    }), client.id);
  });

  // Disconnecting player from other clients
  client.on('disconnected', () => {
    server.writeToAll('disconnected', client.id, client.id);
  });

});

server.listen();
