const getAccessToken = () => {
    if (typeof Storage === 'undefined') {
      return new Error('Storage type not valid');
    }
    return localStorage.getItem('token');
  };

const updateAccessToken = (newToken: string): void => {
    if (typeof Storage === 'undefined') return;
    let token = localStorage.getItem('token');
    token = newToken;
    localStorage.setItem('token', token);
};

const setToken = (token: string) => {
    if (typeof Storage === 'undefined') {
      throw new Error('No valid storage type found');
    }
    localStorage.setItem('token', token);
  };

const removeToken = () => {
  if (typeof Storage === 'undefined') return;
  localStorage.removeItem('token');
}

const TokenService = {
    getAccessToken,
    updateAccessToken,
    setToken,
    removeToken
};

export default TokenService;

