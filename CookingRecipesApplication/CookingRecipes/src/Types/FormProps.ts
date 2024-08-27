export default interface FormProps {
  primary?: boolean;
  id?: string;
  initialValues: any;
  validationSchema?: any;
  onSubmit: (values: any) => void;
  children?: React.ReactNode;
}
