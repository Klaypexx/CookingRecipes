import classNames from "classnames";
import { ErrorMessage, Field } from "formik"
import styles from "./FormInput.module.css"

interface ButtonProps {
    margin?: boolean;
    small?: boolean
    type?: string
    className?: string;
    name: string;
    placeholder: string;
  }

const FormInput: React.FC<ButtonProps> = ({ margin, small, type, className, name, placeholder }) => {
    return (
        <div className={classNames(margin ? styles.baseBoxStyle : undefined, small ? styles.smallBox : undefined)}>
            <Field name={name} type={type} placeholder={placeholder} className={classNames(className, styles.baseFieldStyle, small ? styles.smallBox : undefined )}/>
            <ErrorMessage name={name} component="div" className={styles.baseErrorStyle}/>
        </div>
    )
}

export default FormInput;