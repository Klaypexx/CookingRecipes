import * as Yup from 'yup';
import {
  DESCRIPTION_MAX_WORDS,
  NAME_MAX_WORDS,
  NAME_MIN_WORDS,
  PASSWORD_MAX_WORDS,
  PASSWORD_MIN_WORDS,
  USERNAME_MAX_WORDS,
  USERNAME_MIN_WORDS,
} from '../../../Constants/register';

const userValidation = Yup.object({
  name: Yup.string()
    .required('Имя обязательно')
    .min(NAME_MIN_WORDS, `Минимум ${NAME_MIN_WORDS} символа`)
    .max(NAME_MAX_WORDS, `Максимум ${NAME_MAX_WORDS} символов`),
  userName: Yup.string()
    .required('Логин обязателен')
    .min(USERNAME_MIN_WORDS, `Минимум ${USERNAME_MIN_WORDS} символа`)
    .max(USERNAME_MAX_WORDS, `Максимум ${USERNAME_MAX_WORDS} символов`),
  description: Yup.string().max(DESCRIPTION_MAX_WORDS, `Максимум ${DESCRIPTION_MAX_WORDS} символов`),
  password: Yup.string()
    .min(PASSWORD_MIN_WORDS, `Минимум ${PASSWORD_MIN_WORDS} символов`)
    .max(PASSWORD_MAX_WORDS, `Максимум ${PASSWORD_MAX_WORDS} символов`),
});

export default userValidation;
