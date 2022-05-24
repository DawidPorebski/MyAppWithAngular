# stage 1
FROM node:latest as node
WORKDIR /app
COPY /DevOpsLearningAppWeb .
RUN npm install
RUN node_modules/.bin/ng build --prod

# stage 2
FROM nginx:alpine
COPY --from=node /app/dist/DevOpsLearningAppWeb /usr/share/nginx/html

CMD ["/bin/sh",  "-c",  "envsubst < /usr/share/nginx/html/assets/env.sample.js > /usr/share/nginx/html/assets/env.js && exec nginx -g 'daemon off;'"]
