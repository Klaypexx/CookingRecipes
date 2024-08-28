import BaseField from '../../Field/BaseField/BaseField';
import BaseForm from '../BaseForm/BaseForm';
import styles from './UserForm.module.css';

const UserForm = () => {
  return (
    <div className={styles.userProfileContainer}>
      <div>
        <BaseForm
          initialValues={undefined}
          onSubmit={function (values: any): void {
            throw new Error('Function not implemented.');
          }}
        >
          <BaseField className={styles.field} name="name" labelText="Имя" margin />
          <BaseField className={styles.field} name="username" labelText="Логин" margin />
          <BaseField className={styles.field} name="password" />
        </BaseForm>
      </div>
      <div>о себе</div>
    </div>
  );
};

export default UserForm;
