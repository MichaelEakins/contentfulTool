import React, { useEffect, useState } from 'react';
import { getEntries } from '../contentfulService';

const ContentList: React.FC = () => {
    const [entries, setEntries] = useState<any[]>([]);

    useEffect(() => {
        async function fetchEntries() {
            try {
                const data = await getEntries();
                setEntries(data);
            } catch (error) {
                console.error('Failed to fetch entries:', error);
            }
        }

        fetchEntries();
    }, []);

    return (
        <div>
            <h1>Content Entries</h1>
            <ul>
                {entries.map((entry, index) => (
                    <li key={index}>{entry.fields.title}</li> // Customize based on entry fields
                ))}
            </ul>
        </div>
    );
};

export default ContentList;
