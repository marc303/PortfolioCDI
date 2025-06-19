/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Exceptions;

/**
 *
 * @author Marc
 */
public class NotAlphabetException extends Exception{
    public String messageErreur;
    
    public NotAlphabetException(String messageErreur)
    {
        this.messageErreur = messageErreur;
    }
}
