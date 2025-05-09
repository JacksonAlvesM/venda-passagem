# Sistema de Venda de Passagens de Ônibus

## Sobre o Projeto
Sistema desenvolvido para gerenciar a venda de passagens de ônibus, permitindo consulta de rotas disponíveis, visualização de assentos e compra de passagens.

## Arquitetura
O projeto segue a arquitetura Clean Architecture, sendo estruturado em camadas bem definidas:

- **SellBusTicket.Domain**: Contém as entidades de negócio, objetos de valor (Value Objects), interfaces e regras de domínio.
- **SellBusTicket.Application**: Implementa os casos de uso da aplicação, seguindo o padrão CQRS simplificado.
- **SellBusTicket.Infrastructure**: Responsável pela persistência de dados e implementações concretas dos repositórios.
- **SellBusTicket.Api**: Expõe os endpoints RESTful para consumo dos serviços.
- **SellBusTicket.Tests**: Contém os testes automatizados do projeto.

## Padrões de Projeto Implementados

### Domain-Driven Design (DDD)
- **Entidades**: Place, Route, Seat, Trip
- **Value Objects**: Cpf, PassengerName, SeatNumber
- **Repositories**: Interfaces definidas no domínio e implementadas na infraestrutura

### Clean Architecture
Separação clara entre as camadas com dependências apontando para o centro (domínio).

### Repository Pattern
Abstração da camada de persistência através de interfaces de repositório.

### Notification Pattern
Utilizado para validações e gestão de erros, implementado através do `NotificationContext`.

### Injeção de Dependência
Registro e resolução de dependências através do container nativo do ASP.NET Core.

## Características Técnicas

- **Banco de Dados**: Utiliza Entity Framework Core com banco de dados em memória
- **Documentação API**: Swagger UI integrado
- **Seeding de Dados**: Implementação de dados iniciais para demonstração
- **Validações**: Validações de regras de negócio nos Value Objects

## Funcionalidades Principais

1. **Consulta de Lugares** - Obtém todos os lugares cadastrados
2. **Consulta de Rotas Disponíveis** - Lista rotas disponíveis entre origem e destino
3. **Consulta de Assentos** - Verifica disponibilidade de assentos por rota
4. **Venda de Passagens** - Permite a compra de passagens com validação de CPF e disponibilidade

## Diferenciais

- **Validações Robustas**: Implementação de validações de regras de negócio nos Value Objects
- **Arquitetura Escalável**: Facilidade para adicionar novos recursos sem afetar o código existente
- **Abordagem Orientada a Domínio**: Modelagem de negócio rica e consistente
- **Testabilidade**: Projeto estruturado para facilitar testes automatizados
- **Manutenibilidade**: Baixo acoplamento entre as camadas


## Dicas para execução

- O projeto só precisa ser baixar e inicializado.
- O swagger esta disponivel para testes.

