const getAccessToken = () => {
    if (typeof Storage === 'undefined') {
      return new Error('Storage type not valid');
    }
    return JSON.parse(localStorage.getItem('token') || '{}');
  };

const updateAccessToken = (newToken: string): void => {
    if (typeof Storage === 'undefined') return;
    let token = JSON.parse(localStorage.getItem('token') || '{}');
    token = newToken;
    localStorage.setItem('token', JSON.stringify(token));
};

const setToken = (token: string) => {
    if (typeof Storage === 'undefined') {
      throw new Error('No valid storage type found');
    }
    localStorage.setItem('token', JSON.stringify(token));
  };

const TokenService = {
    getAccessToken,
    updateAccessToken,
    setToken
};

export default TokenService;

