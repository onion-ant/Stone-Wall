import React from 'react';

export const UserContext = React.createContext();

export const UserStorage = ({ children }) => {
  const [data, setData] = React.useState(null);
  const [login, setLogin] = React.useState(null);
  const [loading, setLoading] = React.useState(false);
  const [error, setError] = React.useState(null);

  return (
    <UserContext.Provider value={{ data, error, loading, login }}>
      {children}
    </UserContext.Provider>
  );
};

export default UserContext;
