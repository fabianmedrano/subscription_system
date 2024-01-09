# Use the official image as a parent image
FROM mcr.microsoft.com/mssql/server:2019-latest

# Run the rest of the commands as the `root` user
USER root

# Create a new directory
RUN mkdir -p /usr/src/app

# Set the working directory
WORKDIR /usr/src/app

# Copy the current directory contents into the container at /usr/src/app
COPY . /usr/src/app

# Run the command inside your container filesystem
RUN chmod +x /usr/src/app/do-my-sql-commands.sh

# Run the command on container startup
CMD /bin/bash ./entrypoint.sh

#construir tu imagen Docker
# docker build -t my-sql-server .


#Ejecutar el contenedor 
# docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<welcometohell0>' -p 1433:1433 --name sql_server -d my-sql-server
