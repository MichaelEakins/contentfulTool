// src/App.tsx

import React from 'react';
import ContentManager from './components/ContentManager';

const App: React.FC = () => {
  return (
    <div className="App">
      <header>
        <h1>Welcome to the Content Manager</h1>
      </header>
      <main>
        <ContentManager />
      </main>
    </div>
  );
};

export default App;
