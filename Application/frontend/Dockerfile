# Use Node.js image for building the React app
FROM node:16 AS build

# Set working directory
WORKDIR /ievent-app

# Copy package.json and install dependencies
COPY package*.json ./
RUN npm i

# Copy the source code and build the app
COPY ./* .

RUN npx react-scripts build

# Use an Nginx image to serve the app
FROM nginx:alpine
COPY --from=build /ievent-app/build /usr/share/nginx/html

# Expose the default Nginx port
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
