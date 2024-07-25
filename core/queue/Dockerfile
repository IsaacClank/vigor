FROM redis:7.2.5-bookworm

RUN mkdir /usr/local/etc/redis
COPY ./redis.conf /usr/local/etc/redis/redis.conf

CMD [ "redis-server", "/usr/local/etc/redis/redis.conf" ]
