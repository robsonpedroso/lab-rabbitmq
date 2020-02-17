# Lab - RabbitMQ

Projeto para testar o RabbitMQ, enfileirando e consumindo.

## Introdução

Essas instruções fornecerão uma cópia do projeto em execução na sua máquina local para fins de desenvolvimento e teste.

### Prerequisitos

O que você precisa para baixar, rodar e disponibilizar.

* Dotnet core 3.1
* Vagrant
* VirtualBox
* IDE de sua preferência 

### Instalação

Após a execução do pre requisitos, segue um passo a passo de como rodar localmente.

Clonar o repositório

```
git clone git@github.com:robsonpedroso/lab-rabbitmq.git
```

Rodar o comando `vagrant up` para subir o server com o RabbitMQ instalado
 - IP do servidor `192.168.33.10`
	- Porta do dashboard: 15672
 - Usuário: `admin`
 - Senha: `123`


Abra a solução com o seu IDE (no meu caso Visual Studio) e compile.
 - Pode ser feito pelo bash, terminal ou cmd através do comando `dotnet build`

Execute o projeto `RabbitMQQueue` para enfileirar as mensagens na fila.

Execute o projeto `RabbitMQConsumer` para consumir as mensagens da fila.

## Publicação

Não foi publicado

## Autores

* **Robson Pedroso** - *Projeto inicial* - [RobsonPedroso](https://github.com/robsonpedroso)

## License

Software feito apenas para fins de estudo
