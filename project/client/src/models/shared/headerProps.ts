// source
import { ButtonProps } from "src/models/shared/buttonProps";

export interface HeaderProps {
    header: string;
    buttons: ButtonProps[];
    counter?: number;
}