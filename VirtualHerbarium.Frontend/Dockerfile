#Add nginx to our image

FROM node as build-stage
#Copy our app files from dist folder to our web server created in this image

RUN mkdir /app
WORKDIR /app
COPY . .

#Copy our app files from dist folder to our web server created in this image
RUN npm install 
RUN npm run build
RUN npm install @angular/cli --save-dev

RUN mv nginx /root/
RUN mv dist /root/
RUN cd ..
RUN rm -rf /app

#Expose image ports that are used by our web server so we can access it from outside of image

EXPOSE 80 443

#This final line of code is the ENTRYPOINT, which is a Docker concept.

#It sets the command and parameters that are run when the container is run. In other words, the container starts, and it starts nginx.

FROM nginx:alpine
COPY --from=build-stage /root/nginx/default.conf /etc/nginx/conf.d/
COPY --from=build-stage /root/dist/VirtualHerbarium /usr/share/nginx/html
ENTRYPOINT [ "nginx", "-g", "daemon off;" ]
