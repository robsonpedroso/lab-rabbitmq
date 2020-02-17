# Lab - RabbitMQ

Projeto para testar o RabbitMQ, enfileirando e consumindo.

## Introdu��o

Essas instru��es fornecer�o uma c�pia do projeto em execu��o na sua m�quina local para fins de desenvolvimento e teste.

### Prerequisitos

O que voc� precisa para baixar, rodar e disponibilizar.

* Dotnet core 3.1
* Vagrant
* VirtualBox
* IDE de sua prefer�ncia 

### Instala��o

Ap�s a execu��o do pre requisitos, segue um passo a passo de como rodar localmente.

Clonar o reposit�rio

```
git clone git@github.com:robsonpedroso/lab-rabbitmq.git
```

Rodar o comando `vagrant up` para subir o server com o RabbitMQ instalado
 - IP do servidor `192.168.33.10`
	- Porta do dashboard: 15672
 - Usu�rio: `admin`
 - Senha: `123`


Abra a solu��o com o seu IDE (no meu caso Visual Studio) e compile.
 - Pode ser feito pelo bash, terminal ou cmd atrav�s do comando `dotnet build`

Execute o projeto `RabbitMQQueue` para enfileirar as mensagens na fila.

Execute o projeto `RabbitMQConsumer` para consumir as mensagens da fila.

## Publica��o

N�o foi publicado

## Autores

* **Robson Pedroso** - *Projeto inicial* - [RobsonPedroso](https://github.com/robsonpedroso)

## License

Software feito apenas para fins de estudo
