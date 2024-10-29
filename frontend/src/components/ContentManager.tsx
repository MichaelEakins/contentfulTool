import React, { useEffect, useState } from 'react';
import api from '../services/api';

interface Field {
  id: string;
  name: string;
  type: string;
}

interface ContentType {
  sys?: { id?: string };
  name: string;
  fields: Field[];
}

const ContentManager: React.FC = () => {
  const [contentTypes, setContentTypes] = useState<ContentType[]>([]);

  const fetchContentTypes = async () => {
    try {
      const response = await api.get<ContentType[]>(`${process.env.REACT_APP_API_BASE_URL}/Contentful/contenttypes`);
      setContentTypes(response.data);
    } catch (error) {
      console.error("Error fetching content types:", error);
    }
  };

  useEffect(() => {
    fetchContentTypes();
  }, []);

  return (
    <div>
      {contentTypes.map((type) => (
        <div key={type?.sys?.id || Math.random().toString()}>
          <h2>{type.name || "Unnamed Content Type"}</h2>
          <ul>
            {type.fields?.map((field) => (
              <li key={field.id || Math.random().toString()}>
                {field.name || "Unnamed Field"} - {field.type || "Unknown Type"}
              </li>
            ))}
          </ul>
        </div>
      ))}
    </div>
  );
};

export default ContentManager;
