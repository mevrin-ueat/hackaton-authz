# Dockerfile
# Base: Microcks "uber" distribution (single container, Keycloak off)
# Quay.io is the primary registry; mirrored on Docker Hub if you prefer. 
# https://microcks.io/documentation/references/container-images/
FROM quay.io/microcks/microcks-uber:latest

# Copy your OpenAPI spec into the image
COPY microcks/user-roles-api.yml /opt/artifacts/user-roles-api.yaml

# Optional: allow Microcks to resolve relative $ref from this folder
ENV DEFAULT_ARTIFACTS_REPOSITORY_URL="file:///opt/artifacts"

# Add a small bootstrap script that:
# 1) runs Microcks
# 2) waits until http://localhost:8080 is up
# 3) uploads the OpenAPI artifact
COPY microcks/bootstrap.sh /usr/local/bin/bootstrap.sh

EXPOSE 8080

# Replace the default entrypoint to run our bootstrap

ENTRYPOINT ["/bin/sh", "/usr/local/bin/bootstrap.sh"]
