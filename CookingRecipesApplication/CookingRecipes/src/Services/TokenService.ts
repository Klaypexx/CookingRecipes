const getAccessToken = () => {
  if (typeof Storage === 'undefined') {
    return new Error('Storage type not valid');
  }
  const token = localStorage.getItem('token');
  return token;
};

const updateAccessToken = (newToken: string): void => {
  if (typeof Storage === 'undefined') {
    throw new Error('No valid storage type found');
  }
  localStorage.setItem('token', newToken); // Сразу обновляем токен
};

const setToken = (token: string) => {
  if (typeof Storage === 'undefined') {
    throw new Error('No valid storage type found');
  }
  localStorage.setItem('token', token);
};

const removeToken = () => {
  if (typeof Storage === 'undefined') {
    throw new Error('No valid storage type found');
  }
  localStorage.removeItem('token');
};

const TokenService = {
  getAccessToken,
  updateAccessToken,
  setToken,
  removeToken,
};

export default TokenService;
