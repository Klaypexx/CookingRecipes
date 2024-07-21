/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react';
import { Formik, Form } from 'formik';
import classNames from 'classnames';
import styles from "./BaseFormLabel.module.css"

interface FormProps {
    initialValues: any,
    validationSchema: any,
    handleSubmit: any;
    errorText?: string;
    children?: React.ReactNode;
} 

const BaseForm: React.FC<FormProps> = ({ initialValues, validationSchema, handleSubmit, errorText, children }) => {
  return (
    <Formik
      initialValues={initialValues}
      validationSchema={validationSchema}
      onSubmit={handleSubmit}
    >
      {() => (
        <Form className={classNames(styles.baseFormStyle)}>
          {errorText ? 
            <h4 className={styles.errorText}>{errorText}</h4>
          : null}
          {children}
        </Form>
      )}
    </Formik>
  );
};

export default BaseForm;
