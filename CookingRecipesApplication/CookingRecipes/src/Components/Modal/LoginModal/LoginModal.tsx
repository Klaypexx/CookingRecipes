import ButtonBlock from "../../Button/ButtonBlock/ButtonBlock";
import AuthService from "../../../Services/AuthService";
import useModalStore from "../../../Stores/useModalStore";
import BaseForm from "../../Form/BaseForm/BaseForm";
import FormInput from "../../Form/FormInput/FormInput";
import BaseModal from "../BaseModal/BaseModal"
import * as Yup from 'yup';

interface LoginValues {
    username: string,
    password: string
}

const LoginModal = () => {
    const {unsetAll} = useModalStore();

    const handleLogin = async (values: LoginValues) => {
        const response = await AuthService.login(values.username, values.password);
        if (response.status == 200) {
            unsetAll();
        }
        if (response.status == 400) {
            console.log(response.statusText);
        }
    }
    
    const handleExit = () => {
        unsetAll();
    }
    
    const initialValues = {
        username: '',
        password: '',
    };

    const validationSchema = Yup.object({
        username: Yup.string()
          .required('Логин обязателен')
          .min(3, 'Минимум 3 символа')
          .max(25, 'Максимум 25 символов'),
        password: Yup.string()
          .required('Пароль обязателен')
          .max(8, 'Минимум 8 символов')
          .max(25, 'Максимум 25 символов'),
      });

    return (
        <>
            <BaseModal 
                primary haederText="Войти" 
                hasAccountText="У меня еще нет аккаунта"  
            >
                    <BaseForm
                        initialValues={initialValues}
                        validationSchema={validationSchema}
                        handleSubmit={handleLogin}
                    >
                        <FormInput margin name="username" type="text" placeholder="Логин"/>
                        <FormInput name="password" type="password" placeholder="Пароль"/>
                        <ButtonBlock primaryButtonText="Войти" secondaryButtonText="Отмена" primaryType="submit" onClickSecondary={handleExit} />
                    </BaseForm>
            </BaseModal>
        </>
    )
}

export default LoginModal;