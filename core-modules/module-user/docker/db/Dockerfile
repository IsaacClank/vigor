FROM postgres:16.3-bookworm

ARG POSTGRES_DB=User
ARG POSTGRES_USER=admin
ARG POSTGRES_PASSWORD=admin-password

ENV POSTGRES_DB=$POSTGRES_DB
ENV POSTGRES_USER=$POSTGRES_USER
ENV POSTGRES_PASSWORD=$POSTGRES_PASSWORD

RUN apt update
RUN apt install postgresql-16-wal2json

RUN mkdir -p /etc/postgresql
COPY ./config-server.conf /etc/postgresql/postgresql.conf
COPY ./config-authentication.conf /etc/postgresql/pg_hba.conf

RUN mkdir -p /docker-entrypoint-initdb.d
COPY ./init-db.sh /docker-entrypoint-initdb.d/

CMD ["postgres", "-c", "config_file=/etc/postgresql/postgresql.conf", "-c", "hba_file=/etc/postgresql/pg_hba.conf"]
