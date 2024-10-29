import api from './services/api';

export const getEntries = async () => {
    try {
        const response = await api.get('/Contentful'); // Endpoint from ContentfulController
        return response.data;
    } catch (error) {
        console.error('Error fetching entries:', error);
        throw error;
    }
};

export const createEntry = async (entryData: any) => {
    try {
        const response = await api.post('/Contentful', entryData); // Adjust as needed
        return response.data;
    } catch (error) {
        console.error('Error creating entry:', error);
        throw error;
    }
};
export const getContentTypes = async () => {
    try {
        const response = await api.get('/Contentful/contenttypes');
        return response.data;
    } catch (error) {
        console.error('Error fetching content types:', error);
        throw error;
    }
};
