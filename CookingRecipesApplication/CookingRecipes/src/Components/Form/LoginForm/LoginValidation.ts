import * as Yup from 'yup';

const loginValidation = Yup.object({
  username: Yup.string().required('Логин обязателен').min(3, 'Минимум 3 символа').max(25, 'Максимум 25 символов'),
  password: Yup.string().required('Пароль обязателен').min(8, 'Минимум 8 символов').max(25, 'Максимум 25 символов'),
});

export default loginValidation;
