## LogisticaAPI

Projeto criado para armazenar produtos em um determinado estoque, alocando os produtos por rua e localização nas estantes.

### Conceitos abordados:

- Arquitetura DDD (Domain Driven Design);
- Autenticação com JWT;
- Testes unitários com utilização de XUnit;
- Conexão com banco de dados SQL Server.

### Regras de negócio:

- Para adicionar um produto, é necessário vinculá-lo a uma rua existente, nenhum produto deve ser armazenado sem estar em ao menos uma rua. Em relação a localização (estante, posição) elas não são obrigatórias;
- Uma rua possui vários produtos;
- Um produto possui uma rua e uma localização;

### Regras de utilização:

- Para utilizar qualquer controlador é necessário a autenticação, com exceção do endpoint /login que é utilizado para obter o token de validação e ser utilizado no swagger.
- Use inicialmente o email: admin@logistica.com senha: admin para obter um token e ter acesso aos controllers.
