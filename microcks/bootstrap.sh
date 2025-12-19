
#!/usr/bin/env sh
set -euo pipefail

echo "Starting Microcks..."
# Start Microcks in background (uber image runs a Spring Boot app jar)
# The app jar path is /deployments/app.jar in the uber image (see logs in Getting started).
java -jar /deployments/app.jar &
PID=$!

# Wait until Microcks is reachable
echo "Waiting for Microcks to be ready on http://localhost:8080 ..."
until curl -sSf "http://localhost:8080" > /dev/null; do
  sleep 2
done

# Upload your OpenAPI as the main artifact. See 'Importing Services & APIs' docs.
echo "Importing OpenAPI: /opt/artifacts/user-roles-api.yaml"
curl -sSf -X POST \
  "http://localhost:8080/api/artifact/upload?mainArtifact=true" \
  -F "file=@/opt/artifacts/user-roles-api.yaml"

echo "OpenAPI imported. Microcks is serving mocks. PID=${PID}"
# Bring Microcks to the foreground so container stays alive
wait "$PID"
