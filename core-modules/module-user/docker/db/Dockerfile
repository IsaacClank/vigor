FROM postgres:16.3-bookworm

RUN apt update
RUN apt install postgresql-16-wal2json

RUN mkdir -p /etc/postgresql
COPY ./config-server.conf /etc/postgresql/postgresql.conf
COPY ./config-authentication.conf /etc/postgresql/pg_hba.conf

CMD ["postgres", "-c", "config_file=/etc/postgresql/postgresql.conf", "-c", "hba_file=/etc/postgresql/pg_hba.conf"]