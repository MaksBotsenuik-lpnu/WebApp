import React, { createContext, useState } from 'react';

export const AppContext = createContext();

export const AppProvider = ({ children }) => {
  const [candidates, setCandidates] = useState([]);

  return (
    <AppContext.Provider value={{ candidates, setCandidates }}>
      {children}
    </AppContext.Provider>
  );
};
