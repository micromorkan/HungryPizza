O projeto atualmente possui as seguintes tecnologias:

-C#.Net6 Api Rest
-Dapper
-FluentValidation
-Autenticação JWT (desabilitado)
-Banco de Dados Sql Server

Junto com o projeto vou anexar 2 scripts que serão responsáveis por criar a base de dados e o outro para popular as tabelas que serão utilizadas pelo mesmo. E gostaria de informar que tomei a liberdade de incluir algumas propriedade e regras a mais.

P- Porque o JWT está desabilitado?
R- Como não estava definido no escopo do projeto o uso de um framework de autenticação/autorização, coloquei o sistema pra funcionar de 2 formas diferentes. A primeira (e mais simples) seria os dados do usuário virem junto com a requisição do Pedido. 

Exemplo de cliente já cadastrado:

{
  Dados do Pedido...,
  "userId": 1,    
}

Exemplo de cliente não cadastrado:

{
  Dados do Pedido...,
  "User": {
    "Name": "",
    "Cpf": "",
    "Email": "",
    "ZipCode": "",
    etc...
  }    
}

Seguindo essa linha de raciocínio caso não houvesse Id do cliente, o sistema vai inclui-lo no banco e dar prosseguimento ao registro do Pedido.

Agora, se for necessário, é possível habilitar a Autorização no escopo do projeto. Com isso os dados do Usuario deixariam de vir pelo corpo e passaria a vir pelo Token, onde um Middleware iria fazer toda a validação e consulta/cadastro do usuário.

De resto vou deixar um pequeno exemplo de requisição para registrar um pedido com um usuário existente (sem JWT):

{
  "userId": 1,  
  "orderItems": [
    {      
      "productOrderType": "PIZZA",
      "flavors": [
        "Pepperoni"
      ],
      "quantity": 1
    },
    {      
      "productOrderType": "PIZZA",
      "flavors": [
        "Portuguesa", "Veggie"
      ],
      "quantity": 1
    }
  ],
  "formPayment": "PIX"
}

Qualquer dúvida estou a disposição.
