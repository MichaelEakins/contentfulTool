#!/bin/bash

# Check if jq is installed, as it's required to modify JSON
if ! command -v jq &> /dev/null; then
    echo "jq is required but not installed. Please install jq (e.g., 'brew install jq' on macOS) and rerun this script."
    exit 1
fi

# Define the path to appsettings.json
APPSETTINGS_PATH="./server/appsettings.Development.json"

# Check if appsettings.json exists, if not, create a basic structure
if [ ! -f "$APPSETTINGS_PATH" ]; then
    echo "$APPSETTINGS_PATH file does not exist. Creating one with default structure..."
    mkdir -p ./server
    cat <<EOL > "$APPSETTINGS_PATH"
{
  "Contentful": {
    "SpaceID": "",
    "ManagementApiKey": "",
    "DeliveryApiKey": "",
    "PreviewApiKey": ""
  }
}
EOL
fi

# Prompt the user for each Contentful key
echo "Please enter your Contentful Space ID:"
read -p "Space ID: " SPACE_ID

echo "Please enter your Contentful Management API Key:"
read -p "Management API Key: " MANAGEMENT_API_KEY

echo "Please enter your Contentful Delivery API Key:"
read -p "Delivery API Key: " DELIVERY_API_KEY

echo "Please enter your Contentful Preview API Key:"
read -p "Preview API Key: " PREVIEW_API_KEY

# Use jq to set the values in appsettings.json
jq --arg spaceID "$SPACE_ID" \
   --arg managementKey "$MANAGEMENT_API_KEY" \
   --arg deliveryKey "$DELIVERY_API_KEY" \
   --arg previewKey "$PREVIEW_API_KEY" \
   '.Contentful.SpaceID = $spaceID | .Contentful.ManagementApiKey = $managementKey | .Contentful.DeliveryApiKey = $deliveryKey | .Contentful.PreviewApiKey = $previewKey' \
   "$APPSETTINGS_PATH" > temp.json && mv temp.json "$APPSETTINGS_PATH"

echo "Configuration complete! Your Contentful credentials have been saved in $APPSETTINGS_PATH."
