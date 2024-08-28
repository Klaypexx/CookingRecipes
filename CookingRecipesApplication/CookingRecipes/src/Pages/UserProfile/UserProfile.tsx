import UserForm from '../../Components/Form/UserForm/UserForm';
import Subheader from '../../Components/Subheader/Subheader';
import styles from './UserProfile.module.css';

const UserProfile = () => {
  return (
    <div className={styles.userProfile}>
      <section>
        <Subheader backward text="Мой профиль" />
      </section>
      <section>
        <UserForm />
      </section>
    </div>
  );
};

export default UserProfile;
