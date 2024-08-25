FROM redis:7.2.5-bookworm

RUN ["apt-get", "update"]
RUN ["apt-get", "install", "-y", "gettext-base"]

WORKDIR /usr/share/doc/redis
COPY redis.conf.template /usr/share/doc/redis/redis.conf

WORKDIR /run
COPY run.sh .

CMD ["bash", "/run/run.sh"]
