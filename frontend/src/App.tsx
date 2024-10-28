import React from 'react';
import AddEntryForm from './components/AddEntryForm';
import ContentTypesList from './components/ContentTypesList';

const App: React.FC = () => {
    return (
        <div className="App">
            <h1>Contentful React App</h1>
            <AddEntryForm />
            <ContentTypesList />
            {/* <ContentList /> */}
        </div>
    );
};

export default App;
