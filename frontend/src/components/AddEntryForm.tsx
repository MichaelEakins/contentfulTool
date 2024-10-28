import React, { useState } from 'react';
import { createEntry } from '../contentfulService';

const AddEntryForm: React.FC = () => {
    const [title, setTitle] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const newEntry = {
            fields: { title } // Customize with required fields for Contentful
        };

        try {
            await createEntry(newEntry);
            setTitle(''); // Reset the form
        } catch (error) {
            console.error('Failed to create entry:', error);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>Title:</label>
            <input
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
            />
            <button type="submit">Add Entry</button>
        </form>
    );
};

export default AddEntryForm;
