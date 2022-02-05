# Accounting-CQRS-Kafka
This is the "prove of concept" project. Dockerized Kafka cluster, CQRS approach with materialized views and little stuffs of MongoDb and ElasticSearch are inside.
The idea almost the same to previous project but with dockerized Kafka cluster instead of RabbitMQ and MassTransit.
There are several branches:
- docker-compose branch is identical to master, all containers ochestrated by Docker-compose.
- k8s-docker-desktop - is attempt to orchestrate application containers in kubernetes.
- docker-compose-send-to-partition - in this branch kafka messages written directly to partition and could be read only by specific application, if application is out of service, then message will not be read.
![accounting](https://user-images.githubusercontent.com/50134408/152599068-70d1be5a-1068-4d93-9b20-faea4a2db723.jpg)
