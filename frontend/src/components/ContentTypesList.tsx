import React, { useEffect, useState } from 'react';
import { getContentTypes } from '../contentfulService';

const ContentTypesList: React.FC = () => {
    const [contentTypes, setContentTypes] = useState<any[]>([]);

    useEffect(() => {
        async function fetchContentTypes() {
            try {
                const data = await getContentTypes();
                setContentTypes(data.items); // Assuming Contentful returns an `items` array
            } catch (error) {
                console.error('Failed to fetch content types:', error);
            }
        }

        fetchContentTypes();
    }, []);

    return (
        <div>
            <h2>Content Types</h2>
            <ul>
                {contentTypes.map((contentType, index) => (
                    <li key={index}>{contentType.name}</li>
                ))}
            </ul>
        </div>
    );
};

export default ContentTypesList;
