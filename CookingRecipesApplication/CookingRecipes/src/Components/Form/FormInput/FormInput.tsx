import classNames from "classnames";
import { ErrorMessage, Field } from "formik"
import styles from "./FormInput.module.css"

interface ButtonProps {
    margin?: boolean;
    small?: boolean
    type?: string
    as?: string;
    className?: string;
    name: string;
    placeholder: string;
  }

const FormInput: React.FC<ButtonProps> = ({ margin, type, as, className, name, placeholder }) => {
    return (
        <div className={classNames(margin ? styles.marginBox : undefined, styles.baseBoxStyle, className)}>
            <Field name={name} type={type} as={as} placeholder={placeholder} className={classNames(className, styles.baseFieldStyle )}/>
            <ErrorMessage name={name} component="div" className={styles.baseErrorStyle}/>
        </div>
    )
}

export default FormInput;