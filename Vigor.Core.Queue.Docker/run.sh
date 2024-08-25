#!/bin/bash

envsubst < /usr/share/doc/redis/redis.conf > /usr/local/etc/redis.conf 
redis-server /usr/local/etc/redis.conf
