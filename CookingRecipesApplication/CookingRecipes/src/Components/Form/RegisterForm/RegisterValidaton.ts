import * as Yup from 'yup';

const registerValidation = Yup.object({
  name: Yup.string().required('Имя обязательно').min(3, 'Минимум 3 символа'),
  username: Yup.string().required('Логин обязателен').min(3, 'Минимум 3 символа'),
  password: Yup.string().required('Пароль обязателен').min(8, 'Минимум 8 символов').max(25, 'Максимум 25 символов'),
  confirmPassword: Yup.string()
    .oneOf([Yup.ref('password')], 'Пароли должны совпадать')
    .required('Подтверждение пароля обязательно'),
});

export default registerValidation;
